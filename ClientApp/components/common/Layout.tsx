import * as React from 'react';
import Navbar from './Navbar';

export interface LayoutProps {
    children?: React.ReactNode;
}

export class Layout extends React.Component<LayoutProps, {}> {
    public render() {
        return (
          <div className='container-fluid'>
            <Navbar />
            { this.props.children }
          </div>
        )
    }
}
