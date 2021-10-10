using System.Runtime.CompilerServices;

// ApplicationがDomainのinternalメンバーを使用できるようにする
// Domainのどこかに配置する
[assembly: InternalsVisibleTo("TravelingSalesmanProblem.Application")]