let appInsights = require('applicationinsights');
appInsights
  .setup(process.env.instrumentationKey)
  .setAutoCollectConsole(true)
  .setAutoCollectDependencies(true)
  .setAutoCollectExceptions(true)
  .setAutoCollectPerformance(true, true)
  .setAutoCollectRequests(true)
  .setSendLiveMetrics(true)
  .setUseDiskRetryCaching(true);
appInsights.start();


const controller = require('../controller/auction.controller');


var updateExpiredAuctions = setInterval(() => {
  controller.updateExpiredAuctions();
}, 60000)


let client = appInsights.defaultClient;

var express = require('express');
var router = express.Router();

/* GET home page. */
router.get('/healthcheck', function (req, res, next) {
  client.trackTrace("Auction Service running");
  res.render('index', { title: 'Online Auction Service Working!, version: 1.0' });
});

// Load the MySQL pool connection
const pool = require('../public/data/config');

// Display all Auctions
router.get('/auctions', controller.findAll);

router.get('/auctionsbyBidderId/:userId', controller.findAuctionsByBidderId);
// Display all Winning Bids for user

//Create a new Auction Item
router.post('/auctions', controller.createAuction);

//Update auction data sent through Kafka consumer for topic: bidtopic
router.put('/updateAuctionForBid', controller.updateAuctionForBid);

//Get list of Auctions based on User Id
router.get('/auctionsByUserId/:userId', controller.findAuctionsByUserId);

//Get list of Auctions based on User Id for Payments
router.get('/auctionsForPaymentsByUserId/:userId', controller.findAuctionsForPaymentsByUserId);

//Get list of Auctions based on Auction Id
router.get('/auctionById/:id', controller.findAuctionById);

router.put('/updateAuctionForPayment', controller.updateAuctionForPayment);


module.exports = router;