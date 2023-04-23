package com.onlineauction.bidservice.bidservice;

import org.springframework.core.env.Environment;
import com.onlineauction.bidservice.models.*;
import org.apache.kafka.clients.producer.Callback;
import org.apache.kafka.clients.producer.KafkaProducer;
import org.apache.kafka.clients.producer.ProducerConfig;
import org.apache.kafka.clients.producer.ProducerRecord;
import org.apache.kafka.clients.producer.RecordMetadata;
import org.apache.kafka.common.serialization.LongSerializer;
import org.apache.kafka.common.serialization.StringSerializer;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestHeader;
import org.springframework.web.bind.annotation.ResponseBody;
import java.util.*;
import org.springframework.beans.factory.annotation.Autowired;
import com.google.gson.Gson;
import com.microsoft.applicationinsights.TelemetryClient;
import com.mongodb.*;
import com.mongodb.client.FindIterable;
import com.mongodb.client.MongoCollection;
import com.mongodb.client.MongoDatabase;

@Controller
@RequestMapping("/api/bid")

public class BidController {

    @Autowired
    TelemetryClient telemetryClient;

    @Autowired
    private Environment env;

    @CrossOrigin(origins = "*", allowedHeaders = "*")
    @RequestMapping(value = "/bid", method = RequestMethod.POST)
    @ResponseBody
    public void CreateBid(@RequestHeader("OcRequestId") String correlationId, String bidAmount, String userID,
            String auctionID, String userName) throws Exception {
        try {
            telemetryClient.trackTrace("Creating bid");

            MongoClient mongoClient = new MongoClient(new MongoClientURI(env.getProperty("cosmosdbconnectionstring")));
            MongoDatabase db = mongoClient.getDatabase("biddb");
            MongoCollection<org.bson.Document> coll = db.getCollection("bids");
            org.bson.Document doc = new org.bson.Document();
            doc.append("bidid", UUID.randomUUID().toString());
            doc.append("auctionId", auctionID);
            doc.append("bidAmount", bidAmount);
            doc.append("userId", userID);
            doc.append("userName", userName);
            doc.append("bidDate", System.currentTimeMillis());
            doc.append("correlationId", correlationId);

            try {
                Properties properties = new Properties();
                properties.put("bootstrap.servers", env.getProperty("bootstrap.servers"));
                properties.put("security.protocol", "SASL_SSL");
                properties.put("sasl.mechanism", "PLAIN");
                String jaasConfig = "org.apache.kafka.common.security.plain.PlainLoginModule required username=\"$ConnectionString\""
                        + " password=\"Endpoint=" + env.getProperty("kakfkaendpoint") + ";SharedAccessKeyName="
                        + env.getProperty("kafkasharedaccesskeyname") + ";SharedAccessKey="
                        + env.getProperty("kafkasharedaccesskey") + "\";";

                properties.put("sasl.jaas.config", jaasConfig);
                System.out.println(jaasConfig);

                properties.put(ProducerConfig.KEY_SERIALIZER_CLASS_CONFIG, LongSerializer.class.getName());
                properties.put(ProducerConfig.VALUE_SERIALIZER_CLASS_CONFIG, StringSerializer.class.getName());

                KafkaProducer<Long, String> producer = new KafkaProducer<>(properties);
                long time = System.currentTimeMillis();
                Gson gson = new Gson();
                String bidObjectJson = gson.toJson(doc);
                final ProducerRecord<Long, String> record = new ProducerRecord<Long, String>("bidtopic", time,
                        bidObjectJson);
                producer.send(record, new Callback() {
                    public void onCompletion(RecordMetadata metadata, Exception exception) {
                        if (exception != null) {
                            System.out.println(exception);
                            System.exit(1);
                        }
                    }
                });
                producer.close();

                telemetryClient.trackTrace("Message sent to Kafka bid topic");
            } catch (Exception ex) {
                telemetryClient.trackException(ex);
                System.out.print(ex.getMessage());
                throw ex;
            }

            coll.insertOne(doc);

        } catch (Exception ex) {
            telemetryClient.trackException(ex);
            ex.printStackTrace();
        }
    }

    @CrossOrigin(origins = "*", allowedHeaders = "*")
    @RequestMapping(value = "/healthcheck", method = RequestMethod.GET)
    @ResponseBody
    public String GetRecord() {
        return "Bid Service is working and kafka config is v1";
    }

    @CrossOrigin(origins = "*", allowedHeaders = "*")
    @RequestMapping(value = "/bid", method = RequestMethod.GET)
    @ResponseBody
    public ArrayList<BidDetail> GetBidByAuctionID(String auctionID) {
        ArrayList<BidDetail> lst = new ArrayList<BidDetail>();

        telemetryClient.trackTrace("Getting Bid by Auction ID");

        try {
            MongoClient mongoClient = new MongoClient(new MongoClientURI(env.getProperty("cosmosdbconnectionstring")));
            MongoDatabase db = mongoClient.getDatabase("biddb");
            MongoCollection<org.bson.Document> coll = db.getCollection("bids");
            BasicDBObject doc = new BasicDBObject();
            doc.put("auctionId", auctionID);
            FindIterable<org.bson.Document> cursor = coll.find(doc);

            Integer counter = 1;

            List<org.bson.Document> bsonResult = new ArrayList<org.bson.Document>();
            cursor.into(bsonResult);

            for (org.bson.Document document : bsonResult) {
                BidDetail bidDetail = new BidDetail();
                bidDetail.bidID = counter.toString();
                bidDetail.amount = document.get("bidAmount").toString();
                bidDetail.customer = document.get("userId").toString();
                bidDetail.customerName = document.get("userName").toString();
                bidDetail.bidAt = document.get("bidDate").toString();
                lst.add(bidDetail);
                counter++;
            }

        } catch (Exception ex) {
            telemetryClient.trackException(ex);
            System.out.print(ex.getMessage());
            ex.printStackTrace();

        }

        return lst;

    }

}