--------------------------------------------------------
--  Ref Constraints for Table B_PLAYLIST_STATS
--------------------------------------------------------

  ALTER TABLE "ROCKETLEAGUE"."B_PLAYLIST_STATS" ADD CONSTRAINT "B_PLAYLIST_STATS_FK1" FOREIGN KEY ("PST_REQ_CODE")
	  REFERENCES "ROCKETLEAGUE"."B_REQUESTS" ("REQ_CODE") ON DELETE CASCADE ENABLE;
