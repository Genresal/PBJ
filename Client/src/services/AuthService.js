import {UserManager, WebStorageStateStore} from "oidc-client"

export const userManager = new UserManager({
    userStore: new WebStorageStateStore({ store: window.localStorage }),
    loadUserInfo: true,
    response_mode: "query",
    authority: "https://localhost:7069",
    client_id: "pbj-client",
    response_type: "code",
    scope: "openid profile smsAPI",
    redirect_uri: "http://localhost:3000/callback"
});

export let token;

userManager.getUser().then((user) => {
    if (user) {
        localStorage.setItem("auth", "true");
    }
})