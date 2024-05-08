using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Commands.Comment.Create
{
    public class CreateCommentCommandHandler
        : IRequestHandler<CreateCommentCommand, Infrastructure.Entities.Comment>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public CreateCommentCommandHandler(ICommentRepository repository, IMapper mapper)
        {
            _commentRepository = repository;
            _mapper = mapper;
        }

        public async Task<Infrastructure.Entities.Comment> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
            => await _commentRepository.
            CreateComment(_mapper.Map<Infrastructure.Entities.Comment>(request));
    }
}
