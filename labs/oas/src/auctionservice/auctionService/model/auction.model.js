'use strict';

// Load the MySQL pool connection
const con = require('../public/data/config');

// Auction Object
var Auction = function (auction) {
    this.idAuction = auction.idAuction;
    this.name = auction.name;
    this.description = auction.description;
    this.startingPrice = auction.startingPrice;
    this.auctionDate = auction.auctionDate;
    this.status = auction.status;
    this.image = auction.image;
    this.activeInHours = auction.activeInHours;
    this.bidPrice = auction.bidPrice;
    this.userId = auction.userId;
    this.isActive = auction.isActive;
    this.userName = auction.userName;
    this.isPaymentMade = auction.isPaymentMade;
    this.bidUser = auction.bidUser;
    this.bidId = auction.bidId;
};


Auction.findAll = function (result) {
    con.query('SELECT idAuction, Name, Description, StartingPrice, AuctionDate, Image, userName,  DATEDIFF(date_add(auctiondate, interval activeinhours hour), curdate()) * 24  as ActiveInHours, BidPrice  from auctionservicedb.auction where DATEDIFF(date_add(auctiondate, interval activeinhours hour), curdate()) >=0  and IsActive=1', (err, rows, fields) => {
        console.log("error: ", err);
        if (err) result(err, null);

        console.log(rows);
        result(null, rows);
    });
};

Auction.findAuctionsByBidderId = function (userId, result) {
    con.query("SELECT * from auction where IsActive=1 and bidUser ='" + userId + "'", (err, rows, fields) => {
        if (err) result(err, null);

        console.log(rows);
        result(null, rows);
    });
};

Auction.findAuctionsForPaymentsByUserId = function (userId, result) {
    con.query("SELECT * FROM auction where isActive=0 and (isPaymentMade is null or isPaymentMade=0) and bidUser ='" + userId + "'", (err, rows, fields) => {
        if (err) result(err, null);

        console.log(rows);
        result(null, rows);
    });

};

Auction.findAuctionsByUserId = function (userId, result) {
    con.query("SELECT * FROM auction where userId ='" + userId + "'", (err, rows, fields) => {
        if (err) result(err, null);

        console.log(rows);
        result(null, rows);
    });

};

Auction.findAuctionById = function (id, result) {
    con.query('SELECT name, description,startingPrice, bidPrice, auctionDate, status, image, activeInHours, userId, userName, idAuction FROM auction where idauction =' + id, (err, rows, fields) => {
        if (err) return (err, null);
        console.log(rows);
        result(null, rows);
    });

};

Auction.updateAuctionForBid = function (bidId, bidAmount, userId, auctionId, correlationId, result) {
    con.query("UPDATE auction set  bidId='" + bidId + "', bidPrice=" + bidAmount + ", bidUser='" + userId + "',correlationId='" + correlationId + "', status = 2 where idAuction=" + auctionId, (err, row, fields) => {
        if (err) result(err, null);

        result(null, "updated");
    });
};

Auction.createAuction = function (auction, correlationId, result) {

    let auctionData = [auction.name, auction.description, auction.startingPrice, auction.auctionDate, 1, auction.activeInHours, auction.userId, auction.userName, correlationId];
    let sql = 'INSERT INTO auction(name, description, startingPrice, auctionDate, status, activeInHours, userId, userName, correlationId) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)';

    con.query(sql, auctionData, (err, row, fields) => {
        console.log("error: ", err);
        if (err) result(err, null);

        console.log(row.insertId);
        result(null, row.insertId);
    });

};


Auction.updateExpiredAuctions = function () {

    let sql = 'UPDATE auction set isActive=0 where Now() > DATE_ADD(auctiondate, INTERVAL activeInhours hour) and isActive=1;';
    con.query(sql, (err, rows, fields) => {
        if (err) console.log("some error occured, reason: " + err);

        console.log("updated expired auctions");

    });

};

Auction.updateAuctionForPayment = function (auctionId, correlationId, result) {
    console.log("Inside updateAuctionForPayment model, auction id is " + auctionId + " and correlationId = " + correlationId);
    let sql = "UPDATE auction set  status=3, isPaymentMade=1, correlationId='" + correlationId + "' where idAuction=" + auctionId;
    con.query(sql, (err, row, fields) => {
        if (err) result(err, null);

        result(null, "updated");
    });
};


module.exports = Auction;