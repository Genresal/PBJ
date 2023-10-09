import React, { useContext } from 'react'
import NavMenu from '../UI/NavMenu/NavMenu'
import { PagesContext } from '../modules/Provider/PagesProvider'
import Followers from '../modules/User/components/Followers/Followers'
import { useLocation } from 'react-router-dom/cjs/react-router-dom.min'

export default function FollowersPage() {
  const {user} = useContext(PagesContext)
  const location = useLocation();

  return (
    <>
      <NavMenu/>

      <Followers userEmail={user.email} isFollowers={location.pathname === "/followers"}/>
    </>
  )
}
