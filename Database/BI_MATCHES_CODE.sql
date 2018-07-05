--------------------------------------------------------
--  DDL for Trigger BI_MATCHES_CODE
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "ROCKETLEAGUE"."BI_MATCHES_CODE" 
   before insert on "ROCKETLEAGUE"."B_MATCHES" 
   for each row 
begin  
   if inserting then 
      if :NEW."MTC_CODE" is null then 
         select SEQ_MATCHES.nextval into :NEW."MTC_CODE" from dual; 
      end if; 
   end if; 
end;

/
ALTER TRIGGER "ROCKETLEAGUE"."BI_MATCHES_CODE" ENABLE;
