import Oidc from 'oidc-client';
var mgr = new Oidc.UserManager({
    authority: 'https://localhost:5001',
    client_id: 'spa',
    redirect_uri: 'http://localhost:8080/callback',
    response_type: 'code',
    scope: 'api2 api1 openid profile roles',
    post_logout_redirect_uri: 'http://localhost:8080',
    userStore: new Oidc.WebStorageStateStore({store: window.localStorage}),
    automaticSilentRenew: true,
    silent_redirect_uri: 'http://localhost:8080/static/silent-renew.html',
    accessTokenExpiringNotificationTime: 10,
})
export default mgr;