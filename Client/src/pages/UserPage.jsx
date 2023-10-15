import React, { useEffect, useState } from 'react'
import { useParams } from 'react-router-dom/cjs/react-router-dom.min'
import { getUserAsync } from '../modules/User/api/getUserAsync';
import { useFetching } from '../hooks/useFetching';

export default function UserPage() {
    const params = useParams();
    console.log(params)

    const [user, setUser] = useState();

    const [initializeUser, isLoading] = useFetching(async () => {
        const response = await getUserAsync(params.email);

        if (response) {
            console.log(response)

            setUser(response.data);
        }
    })

    useEffect(() => {
        initializeUser();
    }, [])

    return (
        <div>{isLoading && user.email}</div>
    )
}
