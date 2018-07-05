--------------------------------------------------------
--  DDL for Package Body P_MATCHES
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE BODY "ROCKETLEAGUE"."P_MATCHES" AS

  PROCEDURE BUILD_MATCH_RESULTS(par_req_code in INTEGER) AS
    
    ln_prevMatches INTEGER;
    rMatchStats b_matches%rowtype;
    lb_Found BOOLEAN;
     
    CURSOR cCurMatchStats IS
      SELECT req_wins,
             req_goals,
             req_saves,
             req_assists,
             req_shots,
             req_mvps,
             req_timestamp,
             pst_season_number,
             pst_playlist_id,
             pst_rank_points,
             pst_matches_played,
             pst_tier,
             pst_division
        FROM B_PLAYLIST_STATS JOIN B_REQUESTS ON req_code = pst_req_code
        WHERE req_code = par_req_code;
    rCurMatchStats cCurMatchStats%rowtype;    
        
    CURSOR cPrevMatchStats(p_matches IN INTEGER, p_season_number IN INTEGER, p_playlist_id IN INTEGER) IS
       SELECT req_wins,
             req_goals,
             req_saves,
             req_assists,
             req_shots,
             req_mvps,
             pst_season_number,
             pst_playlist_id,
             pst_rank_points,
             pst_tier,
             pst_division
        FROM B_PLAYLIST_STATS JOIN B_REQUESTS ON req_code = pst_req_code
        WHERE 
            pst_matches_played = p_matches AND
            pst_season_number = p_season_number AND
            pst_playlist_id = p_playlist_id;
     rPrevMatchStats cPrevMatchStats%rowtype;              
  
  BEGIN
    OPEN cCurMatchStats;
    FETCH cCurMatchStats INTO rCurMatchStats;
    CLOSE cCurMatchStats;
    
    ln_prevMatches := rCurMatchStats.pst_matches_played - 1;
    
    OPEN cPrevMatchStats(ln_prevMatches, rCurMatchStats.pst_season_number, rCurMatchStats.pst_playlist_id);
    FETCH cPrevMatchStats INTO rPrevMatchStats;
    IF (cPrevMatchStats%rowcount > 0) THEN
      lb_Found := TRUE;
    END IF;
    CLOSE cPrevMatchStats;
    
    IF (lb_found = TRUE) THEN
        IF (rPrevMatchStats.req_wins < rCurMatchStats.req_wins) THEN 
          rMatchStats.mtc_result := 'WIN';
        ELSE
          rMatchStats.mtc_result := 'LOSS';
        END IF;
        
        rMatchStats.mtc_goals       := rCurMatchStats.req_goals - rPrevMatchStats.req_goals;
        rMatchStats.mtc_assists     := rCurMatchStats.req_assists - rPrevMatchStats.req_assists;
        rMatchStats.mtc_shots       := rCurMatchStats.req_shots - rPrevMatchStats.req_shots;
        rMatchStats.mtc_saves       := rCurMatchStats.req_saves - rPrevMatchStats.req_saves;
        rMatchStats.mtc_mvp         := rCurMatchStats.req_mvps - rPrevMatchStats.req_mvps;
        rMatchStats.mtc_rank_change := rCurMatchStats.pst_rank_points - rPrevMatchStats.pst_rank_points;
        
        rMatchStats.mtc_tier       := rCurMatchStats.pst_tier;
        rMatchStats.mtc_division   := rCurMatchStats.pst_division;
        rMatchStats.mtc_req_code   := par_req_code;
        rMatchStats.mtc_unixStamp  := rCurMatchStats.req_timestamp;
        rMatchStats.mtc_isoStamp   := timestamp '1970-01-01 00:00:00 +00:00' + numtodsinterval(rCurMatchStats.req_timestamp,'second');
        INSERT INTO B_MATCHES VALUES rMatchStats;
    END IF;
  END BUILD_MATCH_RESULTS;
  
  
  PROCEDURE CREATE_MATCHES_FROM_REQUESTS AS
    
    CURSOR cMatches
    IS
    SELECT * FROM B_REQUESTS WHERE req_playlist_type <> 'Multiple' AND req_code NOT IN (SELECT mtc_req_code FROM B_MATCHES);
    rMatches cMatches%rowType;  
    
  BEGIN
    FOR rMatches in cMatches
    LOOP
       BUILD_MATCH_RESULTS(rMatches.req_code);
    END LOOP;
    COMMIT;
  END CREATE_MATCHES_FROM_REQUESTS;


END P_MATCHES;

/
