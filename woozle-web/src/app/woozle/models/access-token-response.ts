export interface AccessTokenResponse {
  accessToken: string,
  expiresIn: number,
  refreshToken: string,
  scope: string,
  tokenType: string
}