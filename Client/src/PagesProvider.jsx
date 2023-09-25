import { UserManager, WebStorageStateStore } from 'oidc-client';
import { useState, createContext } from 'react'
import { useEffect } from 'react';
import axios from 'axios';

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

    const ContextValues = {
        isAuthenticated,
        setIsAuthenticated,
        userManager
    }

    useEffect(() => {
        userManager.getUser().then(user => {
            if(user) {
                setIsAuthenticated(true)

                axios.defaults.headers.common["Authorization"] = `Bearer ${user.access_token}`;
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
