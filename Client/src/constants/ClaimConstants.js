const ClaimType2005Namespace = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims";
const ClaimType2008Namespace = "http://schemas.microsoft.com/ws/2008/06/identity/claims";

export const EmailClaim = ClaimType2005Namespace + "/emailaddress";

export const NameClaim = ClaimType2005Namespace + "/name"

export const SurnameClaim = "surname"

export const BirthDateClaim = ClaimType2005Namespace + "/dateofbirth";

export const RoleClaim =  ClaimType2008Namespace + "/role";