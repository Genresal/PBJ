import { UserManager, WebStorageStateStore } from 'oidc-client';
import { useState, createContext } from 'react'
import { useEffect } from 'react';
import axios from 'axios';
import { decodeToken } from 'react-jwt';
import { EmailClaim } from '../../constants/ClaimConstants';
import { getUserByEmail } from '../User/api/getUserByEmail';
import { useMemo } from 'react';

export const PagesContext = createContext(null);

const userManager = new UserManager({
    userStore: new WebStorageStateStore({ store: window.localStorage }),
    loadUserInfo: true,
    response_mode: "query",
    authority: "https://localhost:7069",
    client_id: "pbj-client",
    response_type: "code",
    scope: "openid profile smsAPI",
    redirect_uri: "http://localhost:3000/callback",
});

export default function PagesProvider({children}) {

    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const [user, setUser] = useState(null);

    const ContextValues = {
        isAuthenticated,
        setIsAuthenticated,
        userManager,
        user,
        setUser
    }

    const getUser = async (access_token) => {
        const decodedToken = decodeToken(access_token);

        const user = await getUserByEmail(decodedToken[EmailClaim]);
        
        return user;
    }

    useEffect(() => {
        userManager.getUser().then(async (user) => {
            if(user) {
                setIsAuthenticated(true)

                axios.defaults.headers.common["Authorization"] = `Bearer ${user.access_token}`;

                setUser(await getUser(user.access_token));
            }
        })
    }, [])

    return (
        <PagesContext.Provider value={ContextValues}>
            {
                children
            }
        </PagesContext.Provider>
    )
}