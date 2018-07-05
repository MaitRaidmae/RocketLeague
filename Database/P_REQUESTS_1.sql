--------------------------------------------------------
--  DDL for Package Body P_REQUESTS
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE BODY "ROCKETLEAGUE"."P_REQUESTS" AS

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
      ) RETURN NUMBER AS
      
      ln_return_value NUMBER;      
  BEGIN
    INSERT INTO B_REQUESTS (
        req_updated_at,
        req_next_update_at,
        req_timestamp,
        req_wins,
        req_goals,
        req_mvps,
        req_saves,
        req_shots,
        req_assists,
        req_playlist_type
        ) 
    VALUES (
        par_req_updated_at,
        par_req_next_update_at,
        par_req_timestamp,
        par_req_wins,
        par_req_goals,
        par_req_mvps,
        par_req_saves,
        par_req_shots,
        par_req_assists,
        par_req_playlist_type) returning req_code into ln_return_value;
    RETURN ln_return_value;
  END INSERT_ROW;

END P_REQUESTS;

/
