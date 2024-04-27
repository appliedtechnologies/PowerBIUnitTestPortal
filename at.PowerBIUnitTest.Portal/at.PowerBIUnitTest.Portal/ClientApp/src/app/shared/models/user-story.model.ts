import { User } from "oidc-client";
import { UnitTest } from "./unit-test.model";
import { TabularModel } from "./tabular-model.model";

export interface UserStory {
    Id?: number;
    UniqueIdentifier?: string;
    Name?: string;
    TabularModel?: number;
    CreatedBy?: number | null;
    CreatedOn?: string | null;
    ModifiedBy?: number | null;
    ModifiedOn?: string | null;
    CreatedByNavigation?: User;
    ModifiedByNavigation?: User;
    UnitTests?: UnitTest[];
    TabularModelNavigation?: TabularModel;
}