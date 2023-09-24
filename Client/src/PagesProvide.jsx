import { UserManager, WebStorageStateStore } from 'oidc-client';
import React, { useState } from 'react'
import { useEffect } from 'react';

export default function PagesProvide({children}) {

    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const [token, setToken] = useState("");

    const ContextValues = {
        isAuthenticated,
        setIsAuthenticated,
    }

    const userManager = new UserManager({
        userStore: new WebStorageStateStore({ store: window.localStorage }),
        loadUserInfo: true,
        response_mode: "query",
        authority: "https://localhost:7069",
        client_id: "pbj-client",
        response_type: "code",
        scope: "openid profile smsAPI",
        redirect_uri: "http://localhost:3000/callback",
        silent_redirect_uri: "http://localhost:3000/refresh"
    });

    userManager.getUser().then((user) => {
        if (user) {
            setToken(user.access_token)
        }
    })

    const getEmailFromToken = () => {

    }
    
    useEffect(() => {
        userManager.signinRedirect();
        
        if(localStorage.getItem("access_token")) {
            setIsAuthenticated(true);
            console.log(1234);
        }
    }, [])

    return (
        <>
            {isAuthenticated ? children : <p>Need to auth</p>}
        </>
    )
}
