export interface Task {
    id: number;
    name: string;
    startDate: string;
    endDate: string;
    priority: string;
    parentTaskName: string;
    parentTaskId: number;
    ownerName: string;
    ownerId: number;
    projectName: string;
    projectId: number;
    statusId: number;
}


export enum TaskStatus {
    Pending = 1,
    InProgress = 2,
    Completed = 3,
    Suspended = 4
}
