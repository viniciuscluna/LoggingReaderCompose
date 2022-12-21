import create from 'zustand'
import { LogData } from '../types/logData'

interface LogState {
  logs: LogData[];
  setLogs: (logs: LogData[]) => void;
  increaseLogs: (logs: LogData[]) => void;
  clearLogs: () => void;
}

export const useLogStore = create<LogState>((set) => ({
  logs: [],
  setLogs: (logsData) => set(() => ({ logs: logsData })),
  increaseLogs: (logsData) => set((state) => ({ logs: state.logs.concat(logsData)})),
  clearLogs: () => set({ logs: [] }),
}))