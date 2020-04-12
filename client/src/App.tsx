import React from 'react';
import { Router, Route, Switch, Link } from 'react-router-dom';
import 'antd/dist/antd.css';
import { Menu, Layout } from 'antd';
import { Logo, Header } from './_components/Header/Header';
import { history } from './_helpers';
import { PrivateRoute } from './_components/PrivateRoute';
import { NotFoundController } from './controller/NotFoundController';
import { AuthController } from './controller/AuthController';
import { LogOutController } from './controller/LogOutController';
import { StudentsController } from './controller/StudentsController';
import { PrivateLink } from './_components/PrivateNavLink';

const { Content, Footer } = Layout;

export const App = ({ ...props }) => {
  return (
    <Layout className="layout">
      <Router
        history={history}
      >
        <Header>
          <Link to="./">
            <Logo />
          </Link>
          <Menu
            mode="horizontal"
            theme="light"
          >
            <Menu.Item key="/logout" >
              <PrivateLink to="logout" exact>
                logout
                </PrivateLink>
            </Menu.Item>
          </Menu>
        </Header>
        <Content style={{ display: 'flex', flexDirection: 'row', flex: '1 1 100%', width: '90wv' }}>
          <Switch>
            <Route path='/auth' component={AuthController} />
            <PrivateRoute path='/logout' component={LogOutController} />
            <PrivateRoute path='/' component={StudentsController} />
            <Route component={NotFoundController} />
          </Switch>
        </Content>
      </Router>
      <Footer style={{ display: 'flex', justifyItems: 'spice-beetwin' }}><div>© Peter 2019 - {(new Date()).getFullYear()}</div>
        <div>developed by Mazin Peter</div></Footer>
    </Layout>
  );
}

export default App;
