import axios, { AxiosRequestConfig } from 'axios';
import { TokenStorage } from '../../core/TokenStorage';
import { Config } from '../../core';
import { IJwtBody } from './JwtBody';

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
            });
    };

    static async logOut(): Promise<any> {
        TokenStorage.clear();
    };
}
