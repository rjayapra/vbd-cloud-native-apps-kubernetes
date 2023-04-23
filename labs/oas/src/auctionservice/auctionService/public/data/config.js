const mysql = require('mysql');

// Set database connection credentials
const config = {

    host: process.env.mysqlhost, //'mysqltfxoas.mysql.database.azure.com', // process.env.mysqlhost,
    user: process.env.mysqlusername,  //'tfxuser@mysqltfxoas' 
    password: process.env.mysqlpassword, //'P@ssw0rd!@#'
    database: process.env.mysqldbname, //'auctionservicedb'
    instrumentationKey: process.env.instrumentationKey //'cf7d4d7c-e190-4842-a4fb-25bf2e3a0f13'


    /*
    host: 'mysqltfxoas.mysql.database.azure.com', // process.env.mysqlhost,
    user: 'tfxuser@mysqltfxoas',
    password: 'P@ssw0rd!@#',
    database: 'auctionservicedb',
    instrumentationKey: 'cf7d4d7c-e190-4842-a4fb-25bf2e3a0f13'


    host: 'ovtfx-auctiondbsrv.mysql.database.azure.com', // process.env.mysqlhost,
    user: 'ovtfx-admin@ovtfx-auctiondbsrv',
    password: 'P@ssw0rd!@#',
    database: 'auctionservicedb',
    instrumentationKey: 'cf7d4d7c-e190-4842-a4fb-25bf2e3a0f13'

    */ 
};

// Create a MySQL pool
const pool = mysql.createPool(config);

// Export the pool
module.exports = pool;