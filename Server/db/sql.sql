CREATE DATABASE IF NOT EXISTS RANKING;
USE     RANKING;
CREATE TABLE RANKING_TABLE
(
    musicID INT,
    userID UUID,
    score INT,
    combo INT
);

CREATE TABLE USER_TABLE
(
    userID UUID,
    userName TEXT
);
