const e = require('express');
const Auction = require('../model/auction.model');

exports.findAll = function (req, res) {
    Auction.findAll(function (err, auctions) {
        if (err) return res.status(500).send('Error occured during fetching auctions');

        console.log('auctions: ', auctions);
        return res.status(200).send(auctions);
    });
};

exports.findAuctionsByBidderId = function (req, res) {
    Auction.findAuctionsByBidderId(req.params.userId, function (err, auctions) {
        if (err) return res.status(500).send('Error occured during fetching auctions');

        console.log('auctions: ', auctions);
        return res.status(200).send(auctions);
    });
};

exports.findAuctionsByUserId = function (req, res) {
    Auction.findAuctionsByUserId(req.params.userId, function (err, auctions) {
        if (err) return res.status(500).send('Error occured during fetching auctions');

        console.log('auctions: ', auctions);
        return res.status(200).send(auctions);
    });
};

exports.findAuctionsForPaymentsByUserId = function (req, res) {
    Auction.findAuctionsForPaymentsByUserId(req.params.userId, function (err, auctions) {
        if (err) return res.status(500).send('Error occured during fetching auctions');

        console.log('auctions: ', auctions);
        return res.status(200).send(auctions);
    });
};

exports.findAuctionById = function (req, res) {
    Auction.findAuctionById(req.params.id, function (err, auctions) {
        if (err) return res.status(500).send('Error occured during fetching auctions');

        console.log('auctions: ', auctions);
        return res.status(200).send(auctions);
    });
};

exports.updateAuctionForBid = function (req, res) {
    Auction.updateAuctionForBid(req.body.bidid, req.body.bidAmount, req.body.userId, req.body.auctionId, req.header("OcRequestId"), function (err, result) {
        if (err) { return res.status(500).send('Error occured during updating auction'); }
        else {

            console.log("Updated record");
            return res.status(204).json(result);
        }
    });
};

exports.createAuction = function (req, res) {
    Auction.createAuction(req.body, req.header("OcRequestId"), function (err, result) {
        if (err) return res.status(500).send('Error occured during inserting auction');

        console.log("Result is " + result);
        return res.status(201).json(result);
    });

};

exports.updateExpiredAuctions = function () {
    console.log("Update Expired Auctions");
    Auction.updateExpiredAuctions();

};

exports.updateAuctionForPayment = function (req, res) {
    console.log("Auction Object is " + JSON.stringify(req.body));
    console.log("Auction ID is " + req.body.IdAuction);
    Auction.updateAuctionForPayment(req.body.IdAuction, req.body.CorrelationId, function (err, result) {
        if (err) {
            return res.status(500).send('Error occured during updating auction for payment status');
        }
        else {
            console.log("Updated payment status controller");
            return res.status(204).json();
        }
    });
}



