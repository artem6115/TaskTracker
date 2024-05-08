using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Queries.Comment.GetCommentsForTask
{
    public class GetCommentsForTaskQueryHandler
        : IRequestHandler<GetCommentsForTaskQuery, List<Infrastructure.Entities.Comment>>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public GetCommentsForTaskQueryHandler(ICommentRepository repository, IMapper mapper)
        {
            _commentRepository = repository;
            _mapper = mapper;
        }
        public async Task<List<Infrastructure.Entities.Comment>> Handle(GetCommentsForTaskQuery request, CancellationToken cancellationToken)
        { 
            var list = await _commentRepository.GetCommentsForTask(request.Id);
            return list;
            }
    }
}
