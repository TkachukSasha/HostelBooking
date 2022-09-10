export interface RegisterPayload{
  email: string,
  password: string,
  role?: string
}

export interface LoginPayload{
  email: string,
  password: string
}

export interface RefreshPayload{
  token: string
}

export interface JwtResponse{
  access_token: string,
  refresh_token: string
}
