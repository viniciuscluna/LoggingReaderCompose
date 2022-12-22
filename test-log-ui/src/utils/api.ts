import axios from "axios"
import { FilterData } from "../types/filterData"
import { CategoryData } from "../types/logData";

const baseApiUrl = 'http://localhost:5160';

const instance = axios.create({
  baseURL: baseApiUrl
});

export const postPersistLogs = async (): Promise<void> => {
  const data = await instance.post('/api/logging/persistLogs');
  return data.data;
}

export const postFilterData = async (filterData: FilterData): Promise<CategoryData[]> => {
  const data = await instance.post('/api/logging/filter', filterData);
  return data.data;
}