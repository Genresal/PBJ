import React from 'react'
import NavMenu from '../UI/NavMenu/NavMenu'
import { useContext } from 'react'
import { PagesContext } from '../modules/Provider/PagesProvider'

export default function TestPage() {

    const {user} = useContext(PagesContext)

    console.log(user);

    return (
        <div>
            <NavMenu/>

            <h1>TEST PAGE</h1>
            
        </div>
    )
}
