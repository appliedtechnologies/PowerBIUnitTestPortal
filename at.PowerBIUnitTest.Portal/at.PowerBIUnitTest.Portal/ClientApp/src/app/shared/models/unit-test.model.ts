import { User } from "oidc-client";
import { TestRun } from "./test-run.model";
import { UserStory } from "./user-story.model";
import { ResultType } from "./result-type.model";

export interface UnitTest {
  Id?: number;
  UniqueIdentifier?: string;
  Name?: string;
  ExpectedResult?: string;
  UserStory?: number;
  DAX?: string;
  ResultType?: number | null;
  DateTimeFormat?: string;
  DecimalPlaces?: string;
  FloatSeparators?: string;
  CreatedBy?: number | null;
  CreatedOn?: string | null;
  ModifiedBy?: number | null;
  ModifiedOn?: string | null;
  CreatedByNavigation?: User;
  ModifiedByNavigation?: User;
  TestRuns?: TestRun[];
  UserStoryNavigation?: UserStory;
  ResultTypeNavigation?: ResultType;
}