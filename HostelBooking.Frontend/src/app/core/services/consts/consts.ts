export enum ApiMethod{
  GET = 'GET',
  POST = 'POST',
  PUT = 'PUT',
  PATCH = 'PATCH',
  DELETE = 'DELETE'
}

export enum SecurityEndpoints{
  Login = 'api/login',
  Register = 'api/register',
  Refresh = 'api/refresh',
  Logout = 'api/logout'
}

export enum CatalogueEndpoints{
  GetRooms = 'api/rooms',
  GetRoom = 'api/rooms/{id}',
  CreateRoom = 'api/rooms',
  UpdateRoom = 'api/rooms',
  DeleteRoom = 'api/rooms/{id}',


  GetCompanies = 'api/companies',
  GetCompany = 'api/company/{id}',
  CreateCompany = 'api/companies',
  UpdateCompany = 'api/companies',
  DeleteCompany = 'api/companies/{id}'
}
