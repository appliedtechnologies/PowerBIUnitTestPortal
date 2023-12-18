import { UnitTest } from "./UnitTest.model";
import { History } from "./History.model";

export class TestRuns 
{
    public id?: number;
    public TimeStamp?: string;
    public Result?: string;
    public Workspace?: string; 
    public TabularModel?: string; 
    public UserStory?: string; 
    public Count?: number; 
    public Type?: string; 
    public Name?: string;

    public HistoriesRun?: History[];
    
    
}
