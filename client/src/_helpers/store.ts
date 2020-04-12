import { createStore, applyMiddleware, Store } from 'redux';
import { rootReducer } from '../_reducers';
import thunkMiddleware from 'redux-thunk';
import { AlertState } from '../_reducers/alert/alert.reducer';
import { Dictionary } from '../core';
import logger from 'redux-logger';
import { StudentStore } from '../_reducers/students/student.reducer';

export interface IStore extends Dictionary<any> {
    alerts: AlertState;
    students: StudentStore;
}
let _store;
if (process.env.NODE_ENV !== 'production') {
    _store = createStore<any, any, any, any>(
        rootReducer,
        applyMiddleware(
            thunkMiddleware,
            logger
        )
    ) as Store<IStore, any>
} else {
    _store = createStore<any, any, any, any>(
        rootReducer,
        applyMiddleware(
            thunkMiddleware,
        )
    ) as Store<IStore, any>
}

export const store = _store;