import axios, { AxiosRequestConfig } from 'axios';
import { TokenStorage } from '../../core/TokenStorage';
import { Config } from '../../core';
import { IJwtBody } from './JwtBody';
import { history } from '../../_helpers';
import { alertInstance } from '../../_actions';

export class authService {
    static async logIn(username: string, password: string): Promise<void> {
        const requestOptions: AxiosRequestConfig = {
            method: 'POST',
            headers: {
                'Content-Type': 'multipart/form-data',
                'accept': 'multipart/form-data'
            },
        };
        const data = new FormData();
        data.append("username", username);
        data.append("password", password);
        data.append("grant_type", 'password');
        await axios.post<IJwtBody>(Config.BuildUrl('/oauth2/token'), data, requestOptions)
            .then(_ => {
                TokenStorage.storeRefreshToken(_.data.refresh_token);
                TokenStorage.storeToken(_.data.access_token);
                history.push('/');
            }).catch((ex) => {
                alertInstance.error(ex);
            });
    };

    static async Reg(name: string, login: string, password: string): Promise<void> {
        await axios.post<IJwtBody>(Config.BuildUrl('/registrations'), {
            name: name,
            login: login,
            password: password
        })
            .then(_ => {
                authService.logIn(login, password);
            }).catch((ex) => {
                alertInstance.error(ex);
            });
    };

    static async logOut(): Promise<any> {
        TokenStorage.clear();
        history.push('/');
    };
}
