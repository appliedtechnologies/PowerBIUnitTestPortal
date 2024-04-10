import { TestRunStructur } from "./TestRunStructur.model";
import { UserStory } from "./UserStory.model";
import { Workspace } from "./workspace.model";

export interface TabularModelStructure {
    DatasetPbId?: string;
    Workspace?: number;
    Name?: string;
    Id?: number;

    items?: TestRunStructur[];
    
}