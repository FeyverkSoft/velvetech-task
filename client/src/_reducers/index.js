import { combineReducers } from 'redux';
import { students } from './students/student.reducer.ts';
import { alerts } from './alert/alert.reducer.ts';

const appReducer = combineReducers({
    alerts,
    students
});

export const rootReducer = (state, action) => {
    return appReducer(state, action)
}