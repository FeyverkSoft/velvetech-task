import * as React from 'react';
import { NavLink, Link, match, LinkProps } from 'react-router-dom';
import * as History from 'history';
import { TokenStorage } from '../core/TokenStorage';


const defIsHidden: Function = (): boolean => {
    return !TokenStorage.isAuthenticated();
};

interface ILinkProps extends LinkProps {
    isHidden?: Function
    activeClassName?: string;
    isActive?<P>(match: match<P>, location: History.Location): boolean;
    location?: History.Location;
    [id: string]: any;
}

export class PrivateNavLink extends React.Component<ILinkProps>{
    render() {
        return (
            defIsHidden()
                ? null
                : <li><NavLink  {...this.props} /></li>
        )
    }
}

export class PrivateLink extends React.Component<ILinkProps>{
    render() {
        return (
            defIsHidden()
                ? null
                : <Link {...this.props} />
        )
    }
}

export class OnlyPublicNavLink extends React.Component<ILinkProps>{
    render() {
        return (
            defIsHidden()
                ? <li> <NavLink  {...this.props} /></li>
                : null
        )
    }
}

