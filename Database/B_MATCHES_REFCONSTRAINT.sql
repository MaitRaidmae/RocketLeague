--------------------------------------------------------
--  Ref Constraints for Table B_MATCHES
--------------------------------------------------------

  ALTER TABLE "ROCKETLEAGUE"."B_MATCHES" ADD CONSTRAINT "B_MATCHES_FK1" FOREIGN KEY ("MTC_REQ_CODE")
	  REFERENCES "ROCKETLEAGUE"."B_REQUESTS" ("REQ_CODE") ON DELETE CASCADE ENABLE;
