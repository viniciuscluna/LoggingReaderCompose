export interface CategoryData {
    name: string;
    lastDate: string;
    lastIp: string;
    lastMessage: string;
    logs: LogData[];
}

export interface LogData {
    category: string;
    message: string;
    ip: string;
    date: string;
}