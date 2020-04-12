import * as React from 'react';
import { Card, Breadcrumb } from 'antd';
import { HomeOutlined } from '@ant-design/icons';
import style from './auth.module.scss';
import { Content } from '../_components/Content/Content';
import { Link } from 'react-router-dom';

export const NotFoundController = () => {
    return (
        <Content>
            <Breadcrumb>
                <Breadcrumb.Item>
                    <HomeOutlined />
                    <Link to={"/"} />
                </Breadcrumb.Item>
                <Breadcrumb.Item href="">
                    Page not found
                    </Breadcrumb.Item>
            </Breadcrumb>
            <div className={style['auth']}>
                <Card
                    title='Page not found'
                >
                    😏
                </Card>
            </div>
        </Content>
    );
}