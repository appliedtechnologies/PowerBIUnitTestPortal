import { UserStory } from "./UserStory.model";

export class TestRunStructur {
    public TimeStamp?: string;
    public Result?: string;
    public Workspace?: string; 
    public TabularModel?: string; 
    public UserStory?: string; 
    public Count?: number; 
    public Type?: string; 
    public Name?: string;
  
    items?: TestRunStructur[];
}