import React from 'react';
import { Link } from 'react-router-dom';

export const Header = () => {
    return (
        <div>
            <nav>
                <ul>
                    <li>
                        <Link to="/">Records</Link>
                    </li>
                    <li>
                        <Link to="/upload">Uploads</Link>
                    </li>
                </ul>
            </nav>
        </div>
    );
}

