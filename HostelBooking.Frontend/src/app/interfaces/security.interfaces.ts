export interface SignInPayload{
  email: string,
  password: string
}

export interface SignUpPayload{
  email: string,
  password: string,
  role?: string
}

export interface RefreshPayload{
  token: string
}

export interface JwtReponse{
  access_token: string,
  refresh_token: string
}
