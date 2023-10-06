import { useContext } from 'react'
import { PagesContext } from '../modules/Provider/PagesProvider';
import { UserManager } from 'oidc-client';

export default function RefreshPage() {
    const {userManager} = useContext(PagesContext);

    return (
        <>
            {new UserManager().signinSilentCallback()}
        </>
    )
}
