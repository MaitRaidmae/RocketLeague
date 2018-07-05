--------------------------------------------------------
--  DDL for Trigger BI_PRIMARY_PLAYLIST_STATS
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "ROCKETLEAGUE"."BI_PRIMARY_PLAYLIST_STATS" 
   before insert on "ROCKETLEAGUE"."B_PLAYLIST_STATS" 
   for each row 
begin  
   if inserting then 
      if :NEW."PST_CODE" is null then 
         select SEQ_PLAYLIST_STATS.nextval into :NEW."PST_CODE" from dual; 
      end if; 
   end if; 
end;

/
ALTER TRIGGER "ROCKETLEAGUE"."BI_PRIMARY_PLAYLIST_STATS" ENABLE;
