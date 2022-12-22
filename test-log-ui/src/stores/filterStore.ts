import create from 'zustand'
import { FilterData } from '../types/filterData'

const defaultState: FilterData = {
    category: '',
    ip: '',
    message: '',
    page: 0,
    quantity: 25,
    end: null,
    start: null,
    initialState: true
}

interface FilterState {
    filter: FilterData;
    setFilter: (defaultState: FilterData) => void;
    increasePage: () => void;
}

export const useFilterStore = create<FilterState>((set) => ({
    filter: defaultState,
    setFilter: (filterData) => set(() => ({ filter: filterData })),
    increasePage: () => set((state) => ({ filter: {...state.filter, page: state.filter.page + 1}}))
}))