--------------------------------------------------------
--  DDL for Package P_REQUESTS
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE "ROCKETLEAGUE"."P_REQUESTS" AS 

  /* TODO enter package declarations (types, exceptions, methods etc) here */ 
   FUNCTION INSERT_ROW(
      PAR_REQ_UPDATED_AT      NUMBER,
      PAR_REQ_NEXT_UPDATE_AT  NUMBER,
      PAR_REQ_TIMESTAMP       NUMBER,
      PAR_REQ_WINS            NUMBER,
      PAR_REQ_GOALS           NUMBER,
      PAR_REQ_MVPS            NUMBER,
      PAR_REQ_SAVES           NUMBER,
      PAR_REQ_SHOTS           NUMBER,
      PAR_REQ_ASSISTS         NUMBER,
      PAR_REQ_PLAYLIST_TYPE   VARCHAR2
      ) RETURN NUMBER;
-----------------------------------------------------------------------------
END P_REQUESTS;

/
