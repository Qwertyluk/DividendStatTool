using Common.Extensions;
using xAPI.Commands;
using xAPI.Responses;
using xAPI.Sync;
using xAPIServices.Contracts;
using xAPIServices.Enums;
using xAPIServices.Exceptions;

namespace xAPIServices
{
    public class XTBService : IXTBService
    {
        private SyncAPIConnector? connector;
        private readonly Server server;

        public XTBService(bool useDemoServer = false)
        {
            server = useDemoServer ? Servers.DEMO : Servers.REAL;
        }

        public void Login(string userID, string password)
        {
            Credentials credentials = new Credentials(userID, password);
            connector = new SyncAPIConnector(server);
            try
            {
                APICommandFactory.ExecuteLoginCommand(connector, credentials, true);
            }
            catch (APIErrorResponse ex)
            {
                throw new XTBLoginException("Login failed.", ex.ErrorCode);
            }
        }

        public IEnumerable<string> GetSymbols(SymbolsGroupName groupName)
        {
            AllSymbolsResponse allSymbolsResponse = APICommandFactory.ExecuteAllSymbolsCommand(connector, true);
            return allSymbolsResponse.SymbolRecords
                .Where(x => x.GroupName == groupName.GetDescription())
                .Select(x => x.Symbol);
        }
    }
}
