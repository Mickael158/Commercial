CREATE TABLE societe(
    idsociete SERIAL PRIMARY KEY,
    nom VARCHAR,
    localisation VARCHAR
);
INSERT INTO societe (nom, localisation) VALUES
    ('ABC Corporation', 'Paris'),
    ('XYZ Industries', 'New York'),
    ('123 Manufacturing', 'Berlin'),
    ('Tech Innovators', 'San Francisco'),
    ('Global Solutions', 'London');


CREATE TABLE personnel(
    id SERIAL PRIMARY KEY,
    matricule VARCHAR UNIQUE,
    nom VARCHAR,
    prenom VARCHAR
);
INSERT INTO personnel (matricule, nom, prenom) VALUES
    ('EMP001', 'Dupont', 'Jean'),
    ('EMP002', 'Martin', 'Sophie'),
    ('EMP003', 'Johnson', 'Michael'),
    ('EMP004', 'Chen', 'Li'),
    ('EMP005', 'Garcia', 'Maria');


CREATE TABLE service(
    idservice SERIAL PRIMARY KEY,
    idsociete INT4 REFERENCES societe,
    nom VARCHAR,
    idresponsable INT4 REFERENCES personnel
);
INSERT INTO service (idsociete, nom, idresponsable) VALUES
    (1, 'Service A', 1),
    (2, 'Service B', 2),
    (3, 'Service C', 3),
    (4, 'Service D', 4),
    (5, 'Service E', 5);

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
    numero VARCHAR UNIQUE,
    idservice INT4 REFERENCES service,
    idproduit INT4 REFERENCES produit,
    qte DOUBLE PRECISION
);

CREATE TABLE reponse_prestataire(
    id SERIAL PRIMARY KEY,
    numerobesoin_service VARCHAR REFERENCES besoin_service(numero),
    idfournisseur INT4 REFERENCES fournisseur,
    proforma VARCHAR
);