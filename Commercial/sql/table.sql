-- nom base = societe

CREATE TABLE societe(
    idsociete SERIAL PRIMARY KEY,
    nom VARCHAR,
    localisation VARCHAR
);
INSERT INTO societe (nom, localisation) VALUES
    ('ABC Corporation', 'Paris');


CREATE TABLE personnel(
    id SERIAL PRIMARY KEY,
    matricule VARCHAR UNIQUE,
    nom VARCHAR,
    prenom VARCHAR,
    pass VARCHAR
);
INSERT INTO personnel (matricule, nom, prenom, pass) VALUES
    ('EMP001', 'Dupont', 'Jean', 'AZERTY'),
    ('EMP002', 'Martin', 'Sophie', 'AZERTY'),
    ('EMP003', 'Johnson', 'Michael', 'AZERTY'),
    ('EMP004', 'Chen', 'Li', 'AZERTY'),
    ('EMP005', 'Garcia', 'Maria', 'AZERTY'),
    ('EMP006', 'Tsotra', 'Tprenom', 'AZERTY');


CREATE TABLE role (
    matricule VARCHAR,
    etat int
);
INSERT INTO role (matricule , etat) VALUES 
('EMP006',1),
('EMP001',21),
('EMP002',11);



CREATE TABLE service(
    idservice SERIAL PRIMARY KEY,
    idsociete INT4 REFERENCES societe,
    nom VARCHAR,
    idresponsable INT4 REFERENCES personnel
);
INSERT INTO service (idsociete, nom, idresponsable) VALUES
    (1, 'Service A', 1),
    (1, 'Service B', 2),
    (1, 'Service C', 3),
    (1, 'Service D', 4),
    (1, 'Service E', 5);


CREATE TABLE fournisseur(
    id SERIAL PRIMARY KEY,
    nom VARCHAR,
    localisation VARCHAR
);
INSERT INTO fournisseur (nom, localisation) VALUES
    ('Fournisseur A', 'Paris'),
    ('Fournisseur B', 'New York'),
    ('Fournisseur C', 'Berlin'),
    ('Fournisseur D', 'San Francisco'),
    ('Fournisseur E', 'London');


CREATE TABLE produit(
    idproduit SERIAL PRIMARY KEY,
    code VARCHAR UNIQUE,
    nom VARCHAR
);
INSERT INTO produit (code, nom) VALUES
    ('P001', 'Produit 1'),
    ('P002', 'Produit 2'),
    ('P003', 'Produit 3'),
    ('P004', 'Produit 4'),
    ('P005', 'Produit 5');


CREATE TABLE besoin_service(
    idbesoin_service SERIAL PRIMARY KEY,
    numero VARCHAR,
    idservice INT4 REFERENCES service,
    idproduit INT4 REFERENCES produit,
    qte DOUBLE PRECISION,
    etat INT default 0
);


CREATE TABLE validate_besoin(
    id SERIAL PRIMARY KEY,
    date DATE NOT NULL,
    numero_besoin_service VARCHAR,
    idpersonnel INT REFERENCES personnel
);


CREATE TABLE reponse_prestataire(
    id SERIAL PRIMARY KEY,
    numerobesoin_service VARCHAR NOT NULL,
    idfournisseur INT4 REFERENCES fournisseur,
    proforma VARCHAR
);


CREATE OR REPLACE VIEW v_validation_by_resp AS
SELECT
    besoin_service.idbesoin_service,
    besoin_service.numero,
    besoin_service.idservice,
    service.nom AS service_nom,
    service.idresponsable AS idresponsable_service,
    personnel.nom AS responsable_nom,
    personnel.prenom AS responsable_prenom,
    besoin_service.idproduit,
    produit.code AS produit_code,
    produit.nom AS produit_nom,
    besoin_service.qte,
    besoin_service.etat
FROM
    besoin_service
JOIN
    service ON besoin_service.idservice = service.idservice
JOIN
    personnel ON service.idresponsable = personnel.id
JOIN
    produit ON besoin_service.idproduit = produit.idproduit;


-- UPDATE besoin_service SET etat = 0 FROM service WHERE besoin_service.idproduit = 2 AND service.idresponsable = 1 AND service.idservice = 1
/* SELECT
    besoin_service.idbesoin_service,
    besoin_service.numero,
    besoin_service.idservice,
    service.nom AS service_nom,
    service.idresponsable AS idresponsable_service,
    personnel.nom AS responsable_nom,
    personnel.prenom AS responsable_prenom,
    besoin_service.idproduit,
    produit.code AS produit_code,
    produit.nom AS produit_nom,
    SUM(besoin_service.qte) AS total_qte,  
    MAX(besoin_service.etat) AS etat_max  
FROM
    besoin_service
JOIN
    service ON besoin_service.idservice = service.idservice
JOIN
    personnel ON service.idresponsable = personnel.id
JOIN
    produit ON besoin_service.idproduit = produit.idproduit
GROUP BY
    besoin_service.idbesoin_service,
    besoin_service.numero,
    besoin_service.idservice,
    service.nom,
    service.idresponsable,
    personnel.nom,
    personnel.prenom,
    besoin_service.idproduit,
    produit.code,
    produit.nom; */
