export interface FilterData {
    category: string;
    message: string;
    ip: string;
    quantity: number;
    page: number;
    start?: string | null;
    end?: string | null;
    initialState: boolean;
}