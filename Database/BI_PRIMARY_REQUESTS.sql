--------------------------------------------------------
--  DDL for Trigger BI_PRIMARY_REQUESTS
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "ROCKETLEAGUE"."BI_PRIMARY_REQUESTS" 
   before insert on "ROCKETLEAGUE"."B_REQUESTS" 
   for each row 
begin  
   if inserting then 
      if :NEW."REQ_CODE" is null then 
         select SEQ_REQUESTS.nextval into :NEW."REQ_CODE" from dual; 
      end if; 
   end if; 
end;

/
ALTER TRIGGER "ROCKETLEAGUE"."BI_PRIMARY_REQUESTS" ENABLE;
