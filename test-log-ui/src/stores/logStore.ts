import create from 'zustand'
import { CategoryData } from '../types/logData'

interface LogState {
  logs: CategoryData[];
  setLogs: (logs: CategoryData[]) => void;
  increaseLogs: (logs: CategoryData[]) => void;
  clearLogs: () => void;
}

export const useLogStore = create<LogState>((set) => ({
  logs: [],
  setLogs: (logsData) => set(() => ({ logs: logsData })),
  increaseLogs: (logsData) => set((state) => ({ logs: state.logs.concat(logsData)})),
  clearLogs: () => set({ logs: [] }),
}))