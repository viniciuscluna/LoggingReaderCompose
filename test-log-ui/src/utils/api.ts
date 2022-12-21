import axios, { AxiosError } from "axios"
import { FilterData } from "../types/filterData"
import { LogData } from "../types/logData";

const baseApiUrl = 'http://localhost:5160';

const instance = axios.create({
  baseURL: baseApiUrl
});

export const postPersistLogs = async (): Promise<void> => {
  const data = await instance.post('/api/logging/persistLogs');
  return data.data;
}

export const postFilterData = async (filterData: FilterData): Promise<LogData[]> => {
  const data = await instance.post('/api/logging/filter', filterData);
  return data.data;
}