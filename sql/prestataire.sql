<<<<<<< HEAD
CREATE TABLE produit(
    code VARCHAR PRIMARY KEY,
=======
CREATE SEQUENCE fournisseur_id_seq;
CREATE SEQUENCE produit_id_seq;

CREATE TABLE fournisseur (
    id VARCHAR DEFAULT 'FRNS' || nextval('fournisseur_id_seq') PRIMARY KEY,
>>>>>>> 5977652ee1d86a0f5aab9eb42770a297280aa713
    nom VARCHAR
);

INSERT INTO fournisseur (nom) VALUES
    ('Fournisseur 1'),
    ('Fournisseur 2'),
    ('Fournisseur 3');

CREATE TABLE produit(
    id SERIAL PRIMARY KEY,
    code VARCHAR DEFAULT 'p' || nextval('produit_id_seq') UNIQUE,
    nom VARCHAR
);

INSERT INTO produit (nom) VALUES
    ('Produit 1'),
    ('Produit 2'),
    ('Produit 3');

CREATE TABLE stock(
    id SERIAL PRIMARY KEY,
    idfournisseur VARCHAR REFERENCES fournisseur,
    date DATE NOT NULL,
    codeproduit VARCHAR REFERENCES produit(code),
    entre DOUBLE PRECISION DEFAULT 0,
    sortie DOUBLE PRECISION DEFAULT 0,
    pu DOUBLE PRECISION DEFAULT 0
);


CREATE VIEW v_etat_stock AS
SELECT
    s.codeproduit,
    SUM(s.entre - s.sortie) AS qterestant,
    SUM(s.entre * s.pu) / SUM(s.entre) AS pump,
    s.idfournisseur
FROM
    stock s
GROUP BY
    s.codeproduit, s.idfournisseur;

INSERT INTO stock(idfournisseur, date, codeproduit, entre, sortie, pu) VALUES
    ('FRNS1', CURRENT_DATE, 'p1', 50, 0, 2000),
    ('FRNS1', CURRENT_DATE, 'p2', 70, 0, 3000),
    ('FRNS1', CURRENT_DATE, 'p3', 90, 0, 7000),

    ('FRNS2', CURRENT_DATE, 'p1', 50, 0, 1000),
    ('FRNS2', CURRENT_DATE, 'p2', 70, 0, 4000),
    ('FRNS2', CURRENT_DATE, 'p3', 90, 0, 5000),

    ('FRNS3', CURRENT_DATE, 'p1', 50, 0, 2000),
    ('FRNS3', CURRENT_DATE, 'p2', 70, 0, 2000),
    ('FRNS3', CURRENT_DATE, 'p3', 90, 0, 9000);


