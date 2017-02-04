﻿CREATE TABLE zms.dbo.Record
(
	N_ZAP BIGINT NOT NULL,
	PR_NOV SMALLINT NOT NULL,
	Patient_ID_PAC NCHAR(36) NOT NULL
);

ALTER TABLE zms.dbo.Record ADD CONSTRAINT PK_Record PRIMARY KEY (N_ZAP);
CREATE UNIQUE INDEX Record_idx ON zms.dbo.Record (N_ZAP ASC);