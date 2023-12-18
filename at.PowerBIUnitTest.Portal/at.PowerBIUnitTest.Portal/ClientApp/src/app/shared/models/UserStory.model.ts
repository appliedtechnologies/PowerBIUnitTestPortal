import { TabularModel } from "./TabularModel.model";
import { UnitTest } from "./UnitTest.model";

export class UserStory 
{
    Id?: number;
    Beschreibung?: string;
    TabularModel?: number;

    TabularModelNavigation?: TabularModel;
    UnitTests?: UnitTest[];
}
